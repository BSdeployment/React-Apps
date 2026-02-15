import { Stack, Button, TextField} from "@mui/material";
import Add from "@mui/icons-material/Add";
import NewSubjectDialog from "./NewSubjectDialog";
import { useState } from "react";
import { useSubjects } from "../../Services/hooks";

function HeaderSubjectSection({onSearch,onAdd}) {



    
    

    const [openNewSubjectDialog, setOpenNewSubjectDialog] = useState(false)
    

    const HandleCLoseDialog = ()=>{
            setOpenNewSubjectDialog(false);
        
    }

    const handleAddSubjectDialog= async (subjetName)=>{
        setOpenNewSubjectDialog(false)
        onAdd(subjetName)
    }


    



    return (
        <div className="text-center container card p-3">
            <h2>הנושאים הנלמדים</h2>
            <Stack spacing={3} alignItems={"center"}
           
            >
                
                <TextField variant="standard" dir="rtl"
                placeholder="חפוש נושאים.."
                onChange={(e)=>onSearch(e.target.value)}
               

                >
                   
                </TextField>
                <Button 
                    variant="outlined" 
                    color="primary"  
                    style={{ maxWidth: "max-content" }} 
                    startIcon={<Add/>}
                    onClick={() => setOpenNewSubjectDialog(true)}
                >
                    נושא חדש
                </Button>
                <NewSubjectDialog 
                    open={openNewSubjectDialog}
                    onClose={HandleCLoseDialog}
                    onAdd={handleAddSubjectDialog}
                  
                />
            </Stack>





        </div>
    )
}

export default HeaderSubjectSection;