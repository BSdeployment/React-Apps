import { callBackend } from "./transport";

/* ---------------- Subjects ---------------- */

export const SubjectService = {
    getAll() {
        return callBackend("GetAllSubjects");
    },

    getById(id) {
        return callBackend("GetSubjectById", { subjectId: id });
    },

    add(name) {
        return callBackend("AddSubject", { name });
    },

    update(id, name) {
        return callBackend("UpdateSubject", {
            id,
            name
        });
    },

    remove(id) {
        return callBackend("RemoveSubject", { subjectId: id });
    }
};


/* ---------------- Chapters ---------------- */

export const ChapterService = {
    getBySubject(subjectId) {
        return callBackend("GetChaptersBySubject", { subjectId });
    },

    add(subjectId, name) {
        return callBackend("AddChapter", {
            subjectId,
            name
        });
    },

    update(id, name, note = null) {
        return callBackend("UpdateChapter", {
            id,
            name,
            note
        });
    },

    remove(id) {
        return callBackend("RemoveChapter", { chapterId: id });
    },

    increment(id) {
        return callBackend("IncrementChapterStudyCount", { chapterId: id });
    },

    decrement(id) {
        return callBackend("DecrementChapterStudyCount", { chapterId: id });
    }
   ,
    

     addChapters(payload) {
        // payload: { subjectId, mode, from, to? }
        return callBackend("AddChapter", payload);
    },

    
};

