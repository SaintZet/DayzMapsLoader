import {Link, Navigation} from "./NavbarStyles";
import React from "react";

export default function Navbar() {
    return (
        <Navigation className="nav">
            <Link href="/" className="main-page">Main page</Link>
            <Link href="/download" className="download-map">Download map</Link>
        </Navigation>
    )
}