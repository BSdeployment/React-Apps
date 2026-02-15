import { Button, FormControlLabel, TextField } from '@mui/material';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import Save from '@mui/icons-material/Save';

import { useState } from 'react';

function NewSubjectDialog({ open, onClose, onAdd }) {

  const [subjectName, setSubjectName] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();          // מונע refresh
    if (!subjectName.trim()) return;

    onAdd(subjectName.trim());
    setSubjectName("");
  };



  return (
    <Dialog open={open} onClose={onClose} dir='rtl' className='text-center'>
      <form onSubmit={handleSubmit}>
        <DialogTitle>נושא חדש</DialogTitle>

        <label className="text-primary fw-bold">שם הנושא:</label>

        <TextField
      
          variant="standard"
          fullWidth
          autoFocus
          className="p-3"
          value={subjectName}
          onChange={(e) => setSubjectName(e.target.value)}
        />

        <Button
          type="submit" 
                    // 🔥 חשוב
          endIcon={<Save />}
          variant="contained"
          className="m-3"
          disabled={!subjectName.trim()}
        >
          שמור נושא
        </Button>
      </form>
    </Dialog>
  );
}

export default NewSubjectDialog;