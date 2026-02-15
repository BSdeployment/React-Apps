import { useBackend } from "./useBackend";
import { SubjectService,ChapterService } from "./services";


export function useSubjects() {
    const getAllBackend = useBackend(SubjectService.getAll);
    const getByIdBackend = useBackend(SubjectService.getById);
    const addBackend = useBackend(SubjectService.add);
    const updateBackend = useBackend(SubjectService.update);
    const removeBackend = useBackend(SubjectService.remove);


    return {
        getAll: getAllBackend.execute,
        getById: getByIdBackend.execute,
        add: addBackend.execute,
        update: updateBackend.execute,
        remove: removeBackend.execute,

        loading:
            getAllBackend.loading ||
            getByIdBackend.loading ||
            addBackend.loading ||
            updateBackend.loading ||
            removeBackend.loading,

        error:
            getAllBackend.error ||
            getByIdBackend.error ||
            addBackend.error ||
            updateBackend.error ||
            removeBackend.error
    };
}


export function useChapters() {
    const getBySubjectBackend = useBackend(ChapterService.getBySubject);
    const addBackend = useBackend(ChapterService.add);
    const updateBackend = useBackend(ChapterService.update);
    const removeBackend = useBackend(ChapterService.remove);
    const incrementBackend = useBackend(ChapterService.increment);
    const decrementBackend = useBackend(ChapterService.decrement);
   const addChptersBackend = useBackend(ChapterService.addChapters);

    return {
        getBySubject: getBySubjectBackend.execute,
        add: addBackend.execute,
        update: updateBackend.execute,
        remove: removeBackend.execute,
        increment: incrementBackend.execute,
        decrement: decrementBackend.execute,
        addChapters: addChptersBackend.execute,

        loading:
            getBySubjectBackend.loading ||
            addBackend.loading ||
            updateBackend.loading ||
            removeBackend.loading ||
            incrementBackend.loading ||
            decrementBackend.loading ||
            addChptersBackend.loading,

        error:
            getBySubjectBackend.error ||
            addBackend.error ||
            updateBackend.error ||
            removeBackend.error ||
            incrementBackend.error ||
            decrementBackend.error
                ||
            addChptersBackend.error
    };
}

