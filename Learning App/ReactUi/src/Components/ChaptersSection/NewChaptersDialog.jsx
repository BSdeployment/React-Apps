import {
    Dialog,
    DialogTitle,
    DialogContent,
    TextField,
    Button,
    Stack
} from "@mui/material";
import Save from "@mui/icons-material/Save";
import { useState, useEffect } from "react";

function NewChaptersDialog({ open, onClose, addChapter }) {
    const [mode, setMode] = useState("single"); // "single" | "range"
    const [from, setFrom] = useState("");
    const [to, setTo] = useState("");

    // ניקוי שדות בפתיחה / שינוי מצב
    useEffect(() => {
        if (open) {
            setFrom("");
            setTo("");
            setMode("single");
        }
    }, [open]);

    const isSingle = mode === "single";

    const canSubmit =
        isSingle
            ? from.trim().length > 0
            : from.trim().length > 0 && to.trim().length > 0;

    const handleSubmit = () => {
        if (!canSubmit) return;

        addChapter({
            mode,
            from,
            to: isSingle ? null : to
        });

        onClose();
    };

    return (
        <Dialog open={open} onClose={onClose} className="text-center">
            <DialogTitle>הוספת פרקים</DialogTitle>

            <DialogContent>
                {/* בחירת מצב */}
                <Stack direction="row" spacing={3} dir="rtl" justifyContent="center">
                    <label>
                        <input
                            type="radio"
                            checked={isSingle}
                            onChange={() => setMode("single")}
                        />
                        פרק בודד
                    </label>

                    <label>
                        <input
                            type="radio"
                            checked={!isSingle}
                            onChange={() => setMode("range")}
                        />
                        טווח
                    </label>
                </Stack>

                {/* קלטים */}
                <div dir="rtl" className="my-4">
                    <TextField
                        variant="standard"
                        placeholder={isSingle ? "שם פרק" : "התחלה"}
                        value={from}
                        autoFocus
                        onChange={(e) => setFrom(e.target.value)}
                        fullWidth
                    />

                    {!isSingle && (
                        <>
                            <br />
                            <br />
                            <TextField
                                variant="standard"
                                placeholder="סיום"
                                value={to}
                                onChange={(e) => setTo(e.target.value)}
                                fullWidth
                            />
                        </>
                    )}
                </div>

                {/* כפתור */}
                <Button
                    startIcon={<Save />}
                    variant="outlined"
                    onClick={handleSubmit}
                    disabled={!canSubmit}
                >
                    הוסף
                </Button>
            </DialogContent>
        </Dialog>
    );
}

export default NewChaptersDialog;
