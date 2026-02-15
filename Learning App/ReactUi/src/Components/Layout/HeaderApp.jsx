import { AppBar, Typography, Toolbar, IconButton } from "@mui/material"
import { NavLink } from "react-router-dom";

function HeaderApp() {

    return (
        <div>
            <AppBar position="static" >
                <Toolbar style={{ direction: 'rtl' }} className="mx-auto">
                    <Typography variant="h6">חזרות ולימודים</Typography>
                    <div className="mx-auto" >
                        <ul className="my-auto nav"  >
                            <NavLink to="" className="nav-link text-white me-3">בית</NavLink>
                            <NavLink to="about" className="nav-link text-white">אודות</NavLink>
                        </ul>
                    </div>


                </Toolbar>

            </AppBar>
        </div>
    )
}

export default HeaderApp;