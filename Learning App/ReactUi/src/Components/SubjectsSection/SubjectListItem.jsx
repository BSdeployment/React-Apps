import {Divider, Stack,Button, useMediaQuery, useTheme } from "@mui/material";
import  Delete  from "@mui/icons-material/Delete";
import { useNavigate } from "react-router-dom";
import DeleteSubjectDialog from "./DeleteSubjectDialog";
import { useState } from "react";

function SubjectListItem({subjectItem,onDeleteSubject}){

    const navigate = useNavigate();

    const handleNavigate = () => {
        navigate(`/chapters/${subjectItem.id}`);
  

       
    };

    const[openDeleteDialog,setOpenDeleteDialog] = useState(false)
 

    const handleDeleteDialogOpen= (e)=>{
        e.stopPropagation()
        setOpenDeleteDialog(true)
        setItemToDelete(subjectItem)
    }
    const handleDeleteDialogClose = (e)=>{
        e.stopPropagation()
        setOpenDeleteDialog(false)
        
    }


   const handleDelete = (e,id) => {
    e.stopPropagation();
    onDeleteSubject(id);
    setOpenDeleteDialog(false);
};

     const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down("sm"));



    return(
        <Stack 
        onClick={handleNavigate}
        
        alignItems={"center"} direction={"row"} dir="rtl" className="card justify-content-around item-subject-hover mb-2" style={{padding:"10px"}}
        divider={<Divider orientation="vertical" flexItem/>}
        
        >
            <h6 style={{fontSize:"16px"}}>{subjectItem.name}</h6>
            <h6 style={{fontSize:"12px"}}>
 {new Date(subjectItem.date).toLocaleDateString("he-IL", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit"
  })}

            </h6>
            <Button onClick={handleDeleteDialogOpen} style={{maxWidth:"max-content"}}  size={isMobile ? "small" : "medium"} variant="contained" color="warning">
               {isMobile ? (<Delete fontSize="10px"/> ) : (<div>מחק נושא <Delete/></div>)}
            </Button>

            <DeleteSubjectDialog onDelete={handleDelete} openDialog={openDeleteDialog} onCloase={handleDeleteDialogClose}  subjectItem={subjectItem}/>
        </Stack>
    )
}
export default SubjectListItem;