import { Stack, TextField, Button } from "@mui/material";
import { useState } from "react";
import NewChaptersDialog from "./NewChaptersDialog";
import { useChapters, useSubjects } from "../../Services/hooks";
import { useEffect } from "react";


function HeaderChapters({ onSearch, subject,onChaptersChanged }) {
  const [isDialogOpen, setIsDialogOpen] = useState(false);


const [subjectName,setSubjectName] = useState("")
  const openDialog = () => setIsDialogOpen(true);
  const closeDialog = () => setIsDialogOpen(false);
const {getById}  = useSubjects()
  const {addChapters} = useChapters();
    // כאן בהמשך תהיה קריאה ל־Backend
const handleAddChapter = async (payload) => {
  closeDialog();

  await addChapters({
    subjectId: Number(subject.subjectid),
    mode: payload.mode,
    from: payload.from,
    to: payload.mode === "range" ? payload.to : null
  });

  onChaptersChanged(Number(subject.subjectid)); // קריאה לפונקציה שמועברת מה־MainChapterSection כדי לעדכן את הרשימה לאחר הוספה
  // לאחר ההוספה, אפשר להציג הודעה או לרענן את הרשימה

};

const handleGetSubjectName = async (id)=>{
  let res = await  getById(id)
   setSubjectName(res.name)
}

useEffect(()=>{
handleGetSubjectName(Number(subject.subjectid))

},[])
  

  return (
    <div className="text-center container card p-3">
      <h2 style={{ fontSize: "16px" }}>
        פרקים / סימנים בנושא {subjectName}
      </h2>

      <Stack spacing={3} alignItems="center">
        <TextField
          variant="standard"
          dir="rtl"
          placeholder="חיפוש.."
          size="small"
          onChange={(e) => onSearch(e.target.value)}
        />

        <Button
          onClick={openDialog}
          size="small"
          variant="outlined"
          color="primary"
          style={{ maxWidth: "max-content" }}
        >
          פרקים חדשים
        </Button>

        <NewChaptersDialog
          open={isDialogOpen}
          onClose={closeDialog}
          addChapter={handleAddChapter}
        />
      </Stack>
    </div>
  );
}

export default HeaderChapters;
