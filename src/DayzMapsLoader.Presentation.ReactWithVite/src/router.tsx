import {Routes, Route} from "react-router-dom";
import {Home} from "./pages/Home";
import {Download} from "./pages/Download";
import {NotFound} from "./pages/NotFound";

export const Router = () => {
    return (

                <Routes>
                    <Route path="/" index element={<Home/>}/>
                    <Route path="/download" element={<Download/>}/>
                    <Route path="*" element={<NotFound/>}/>
                </Routes>
    );
};