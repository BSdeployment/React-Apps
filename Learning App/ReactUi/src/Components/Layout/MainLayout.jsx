import { CssBaseline } from "@mui/material"
import HeaderApp from "./HeaderApp"
import MainContent from "./MainContent";
import FooterApp from "./FooterApp";
function MainLayout() {


    return (
        <div style={{display:"flex",flexDirection:"column",minHeight:"100vh"}}>
           
            <header>
                    <HeaderApp/>  
            </header>

            <main className="mt-3 p-3 mb-2" style={{flex:1,minHeight:"auto"}}>
                <MainContent/>
            </main>


            <footer className="mt-auto bg-primary">
                    <FooterApp/>
            </footer>

        </div>

    )
}

export default MainLayout;