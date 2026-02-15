import SubjectListItem from "./SubjectListItem";



function ContainerListSubjects({listSubject,onDeleteSubject}){

    return(
        <div className="col-10 col-md-5 card mx-auto p-3 mt-3" style={{minHeight:"400px",maxHeight:"600px",overflowY:"scroll"}}>
            {listSubject.map(s=>(
                <SubjectListItem subjectItem={s} key={s.id} onDeleteSubject={onDeleteSubject}/>
         ))}
        </div>
    )
}

export default ContainerListSubjects;