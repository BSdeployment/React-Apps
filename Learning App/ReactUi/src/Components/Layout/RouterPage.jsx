import { BrowserRouter,Routes,Route } from "react-router-dom"
import HomePage from "./HomePage"
import AboutPage from "./AboutPage";
import App from "../../App";
import MainChapterSection from "../ChaptersSection/MainChapterSection";

function RouterPage(){


    return(
        <>
            <BrowserRouter basename="/index.html" >
            {/* <BrowserRouter  > */}
                <Routes>
                    <Route path="/" element={<App/>}>
                        <Route index element={<HomePage/>}/>
                        <Route path="about" element={<AboutPage/>}/>
                        <Route path="chapters/:subjectid" element={<MainChapterSection/>}/>
                    </Route>
                    
                </Routes>

            </BrowserRouter>
        </>
    )
}

export default RouterPage;

