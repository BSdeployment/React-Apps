import HeaderSubjectSection from "./HeaderSubjectSection";
import ContainerListSubjects from "./ContainerListSubjects";
import { useState } from "react";
import { useChapters, useSubjects } from "../../Services/hooks";
import { useEffect } from "react";
function MainSubjectsSection() {
    const [subjects, setSubjects] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);


  const {getAll,remove,add} = useSubjects()


  useEffect(() => {
    let isMounted = true;

    setLoading(true);

    getAll()
      .then(data => {
      console.log("Fetched subjects:", data);
        if (isMounted) setSubjects(data);
      })
      .catch(err => {
        if (isMounted) setError(err?.message ?? "Failed to load subjects");
      })
      .finally(() => {
        if (isMounted) setLoading(false);
      });

    return () => {
      isMounted = false;
    };
  }, []);

  useEffect(() => {
  setFilteredSubjects(subjects);
}, [subjects]);

  



  const [filteredSubjects, setFilteredSubjects] = useState(subjects);

  const searchWord = (word) => {
    if (!word.trim()) {
      setFilteredSubjects(subjects);
      return;
    }

    setFilteredSubjects(
      subjects.filter(s => s.name.includes(word))
    );
  };


  const handleDeleteSubject = async (id) => {
  const prev = subjects;


  // optimistic
  setSubjects(s => s.filter(x => x.id !== id));

  try {
    await remove(id);
  } catch (err) {
    // rollback
    setSubjects(prev);
    console.error(err);
  }
};


const handleAddSubject = async (name) => {
  try {
    await add(name);
    const fresh = await getAll();
    setSubjects(fresh);
  } catch (err) {
    console.error(err);
  }
  console.log(
  subjects.map(s => s.id)
);
};


  return (
    <div>
      <HeaderSubjectSection onSearch={searchWord} onAdd={handleAddSubject} />
      <ContainerListSubjects listSubject={filteredSubjects} onDeleteSubject={handleDeleteSubject} />
    </div>
  );
}


export default MainSubjectsSection;