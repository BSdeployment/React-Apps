using LearningApp.Application.Services;
using LearningApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static LearningApp.WinForms.RequestServices.WebViewMessage;

namespace LearningApp.WinForms.RequestServices
{

        public class WebViewMessageDispatcher
    {
        private readonly ILearningServices _services;

        private readonly Dictionary<string, Func<JsonElement, Task<object>>> _handlers;

        public WebViewMessageDispatcher(ILearningServices services)
        {
            _services = services;

            _handlers = new Dictionary<string, Func<JsonElement, Task<object>>>
        {
            { "GetAllSubjects", HandleGetAllSubjects },
            { "GetSubjectById", HandleGetSubjectById },
            { "AddSubject", HandleAddSubject },
            { "RemoveSubject", HandleRemoveSubject },

            { "GetChaptersBySubject", HandleGetChapters },
            { "AddChapter", HandleAddChapter },
            { "IncrementChapterStudyCount", HandleIncrement },
            { "DecrementChapterStudyCount", HandleDecrement },
             { "RemoveChapter", HandleRemoveChapter },

        };
        }

        

        public async Task<WebViewResponse> DispatchAsync(WebViewRequest request)
        {

            if (!_handlers.TryGetValue(request.Action, out var handler))
            {
                return new WebViewResponse
                {
                    RequestId = request.RequestId,
                    Success = false,
                    Error = "Action not allowed"
                };
            }

            try
            {
                var payloadJson = (JsonElement)request.Payload;
                var result = await handler(payloadJson);

                return new WebViewResponse
                {
                    RequestId = request.RequestId,
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new WebViewResponse
                {
                    RequestId = request.RequestId,
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        // ===== Handlers =====

        private Task<object> HandleGetAllSubjects(JsonElement _)
            => Task.FromResult<object>(_services.GetAllSubjectsAsync().Result);

        private async Task<object> HandleGetSubjectById(JsonElement payload)
        {
           
            int id = payload.GetProperty("subjectId").GetInt32();
            return await _services.GetSubjectByIdAsync(id);
        }

        private async Task<object> HandleAddSubject(JsonElement payload)
        {
            string name = payload.GetProperty("name").GetString();

            var subject = new LearningSubject { Name = name };
            return await _services.AddSubjectAsync(subject);
        }

        private async Task<object> HandleRemoveSubject(JsonElement payload)
        {
            int id = payload.GetProperty("subjectId").GetInt32();
            return await _services.RemoveSubjectAsync(id);
        }

        private async Task<object> HandleGetChapters(JsonElement payload)
        {
            
            int subjectId = payload.GetProperty("subjectId").GetInt32();
            return await _services.GetChaptersBySubjectAsync(subjectId);
        }

        private async Task<object> HandleAddChapter(JsonElement payload)
        {
            var req = new AddChaptersRequest
            {
                SubjectId = payload.GetProperty("subjectId").GetInt32(),
                Mode = payload.GetProperty("mode").GetString(),
                From = payload.GetProperty("from").GetString(),
                To = payload.TryGetProperty("to", out var toProp)
                    ? toProp.GetString()
                    : null
            };

            await _services.AddChaptersAsync(req);

            return true;
        }



        private async Task<object> HandleIncrement(JsonElement payload)
        {
            int id = payload.GetProperty("chapterId").GetInt32();
            await _services.IncrementChapterStudyCountAsync(id);
            return true;
        }

        private async Task<object> HandleDecrement(JsonElement payload)
        {
            int id = payload.GetProperty("chapterId").GetInt32();
            await _services.DecrementChapterStudyCountAsync(id);
            return true;
        }

        private LearningChapter CreateChapter(int subjectId, string name)
        {
            return new LearningChapter
            {
                SubjectId = subjectId,
                Name = name
            };
        }
        private async Task<object> HandleRemoveChapter(JsonElement payload)
        {
            int chapterId = payload.GetProperty("chapterId").GetInt32();

            var success = await _services.RemoveChapterAsync(chapterId);

            if (!success)
                throw new Exception("Chapter not found");

            return true;
        }


    }

}

