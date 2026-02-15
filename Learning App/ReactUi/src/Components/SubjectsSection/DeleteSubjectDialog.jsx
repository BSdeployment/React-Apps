import { Dialog, DialogContent, DialogActions, DialogTitle, Button } from "@mui/material"




function DeleteSubjectDialog({ openDialog, onCloase, onDelete, subjectItem }) {

    const handleDelete = (e,id) => {
      
      onDelete(e,id)
};

return (
    <Dialog dir="rtl" open={openDialog} onClose={onCloase}>
        <DialogTitle>
            אזהרה
        </DialogTitle>
        <DialogContent>
            האם אתה בטוח שבצרצונך למחוק את {subjectItem.name}?
        </DialogContent>
        <DialogActions>
            <Button onClick={(e)=>{handleDelete(e,subjectItem.id)}} on>כן</Button>
            <Button onClick={onCloase}>לא</Button>

        </DialogActions>
    </Dialog>

)
}
export default DeleteSubjectDialog;