import { useParams } from "react-router-dom";
import HeaderChapters from "./HeaderChapters";
import TableChapters from "./TableChapters";
import { useState } from "react";
import { useChapters } from "../../Services/hooks";
import { useEffect } from "react";
import ChaptersStatistics from "./ChaptersStatistics";

function MainChapterSection(){

    const subject= useParams()

    const {getBySubject,remove, increment,decrement} = useChapters();
    const [chapters, setChapters] = useState([]);

    // const [chapter,setChapter]= useState([
    //     {
    //         id:1,
    //         subjectId:subject.subjectid,
    //         name:"פרק א",
    //         TimesStudied:3,
    //         rate:4,
    //         note:"חשוב מאוד",
    //         orderindex:1
    //     },
    //     {
    //         id:2,
    //         subjectId:subject.subjectid,
    //         name:"פרק ב",   
    //         TimesStudied:5,
    //         rate:5,
    //         note:"חשוב מאוד",
    //         orderindex:2
    //     }
    //     ,
    //     {
    //         id:3,
    //         subjectId:subject.subjectid,
    //         name:"פרק ג",   
    //         TimesStudied:2,
    //         rate:3,
    //         note:"חשוב מאוד",
    //         orderindex:3
    //     }
    // ])

const loadChapters = async () => {
    const data = await getBySubject(Number(subject.subjectid));
    setChapters(data);
    setFilteredChapters(data);
   
  };

useEffect(() => {
    if (!Number.isNaN(subject.subjectid)) {
      loadChapters();
    }
  }, [subject.subjectid]);

 const [filteredChapters, setFilteredChapters] = useState(chapters);
    
      const searchWord = (word) => {
        if (!word.trim()) {
          setFilteredChapters(chapters);
          return;
        }
    
        setFilteredChapters(
         chapters.filter(s => s.name.includes(word)))
        ;
      };


      const deleteChapter = async(chapterId)=>{
       await   remove(chapterId);
        await  loadChapters();
      }

      const HandleIncreas = async (id) =>{
        await increment(id);
        await loadChapters();
      }

      const handleDecrease = async (id)=>{
        await decrement(id);
        await loadChapters()
      }

    return(
            <div>
                <HeaderChapters onSearch={searchWord} subject={subject} onChaptersChanged={loadChapters}/>
                <ChaptersStatistics chapters={chapters}/>
                <TableChapters chaptersList={filteredChapters} onIncrease={HandleIncreas} 
                onDecrease={handleDecrease}
                onDeleteItem={deleteChapter}/>
            </div>
    )
}

export default MainChapterSection;