import React from 'react'
import ReactDOM from 'react-dom/client'
import {Router} from "../router";
import {BrowserRouter} from "react-router-dom";
import Navbar from "../ui/navigation/Navbar";
import Footer from "../ui/footer/Footer";
import {Delimiter} from "../ui/footer/FooterStyles";


ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
        <BrowserRouter>
            <Navbar/>
            <Delimiter/>
            <Router/>
            <Footer/>
        </BrowserRouter>
)
