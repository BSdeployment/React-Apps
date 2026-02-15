import {  Stack, Table, TableBody, TableCell, TableHead, TableRow } from "@mui/material"
import Add from "@mui/icons-material/Add";
import  Remove  from "@mui/icons-material/Remove";
import Delete from "@mui/icons-material/Delete";


function TableChapters({ chaptersList ,onDeleteItem,onIncrease,onDecrease}) {


    return (
        <>

            <div dir="rtl" className="col-12 col-md-4 mx-auto mt-3">
                <Table size="small" className="table table-hover table-bordered  mx-auto" >
                    <TableHead>
                        <TableRow>
                            <TableCell>שם פרק\סימן</TableCell>
                            <TableCell>מספר חזרות</TableCell>
                            {/* <TableCell>דירוג</TableCell> */}
                            <TableCell className="text-center">פעולות</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>

                        {chaptersList.map(c => (
                            <TableRow className="text-center" key={c.id}>
                                <TableCell className="text-center">{c.name}</TableCell>



                                <TableCell className="text-center">{c.timesStudied}</TableCell>
                                {/* <TableCell className="text-center">{c.learningRate}</TableCell> */}
                                <TableCell >
                                    <Stack  direction={"row"} className="text-center">
                                        {/* <Button size={"small"}  variant="contained">
                                            <Add fontSize="10px" />
                                        </Button> */}
                                        <button onClick={()=>onIncrease(c.id)} className="btn btn-primary rounded-circle" style={{maxWidth:"max-content",maxHeight:"max-content",padding:"5px 10px"}}>
                                            <Add fontSize="16px"/>
                                        </button>
                                        <button onClick={()=>onDecrease(c.id)} className="btn btn-warning rounded-circle mx-1" style={{maxWidth:"max-content",maxHeight:"max-content",padding:"5px 10px"}}>
                                             <Remove fontSize="16px"/>
                                        </button>
                                        <button onClick={()=>onDeleteItem(c.id)} className="btn btn-danger rounded-circle" style={{maxWidth:"max-content",maxHeight:"max-content",padding:"5px 10px"}}>
                                            <Delete/>
                                        </button>
                                    </Stack>

                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </div>


        </>

    )
}
export default TableChapters;