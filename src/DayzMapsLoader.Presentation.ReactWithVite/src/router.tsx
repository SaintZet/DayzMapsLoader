import {Routes, Route} from "react-router-dom";

import {Home} from "./pages/Home";
import {Download} from "./pages/Download";
import {NotFound} from "./pages/NotFound";
import {providedMaps, ProvidedMapsContext} from "./modules/DownloadMap/context/providedMapsContext";

export const Router = () => {
    return (
        <ProvidedMapsContext.Provider value={providedMaps}>
            <Routes>
                <Route path="/" index element={<Home/>}/>
                <Route path="/download" element={<Download/>}/>
                <Route path="*" element={<NotFound/>}/>
            </Routes>
        </ProvidedMapsContext.Provider>
    );
};