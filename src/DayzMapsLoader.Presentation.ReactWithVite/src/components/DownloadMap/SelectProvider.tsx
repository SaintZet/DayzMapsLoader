import {MapProvider} from "../../helpers/types";
import LoaderSelector from "../../ui/DownloadMap/LoaderSelector";
import {switchType} from "../../modules/DownloadMap/helpers/switchLoaderDataType";
import React, {ReactNode} from "react";

interface ProviderProps {
    providers: MapProvider[],
}

export default function SelectProvider(props: ProviderProps) {
    const providers = props.providers;
    const loaderSettings = {m: 3, width: 300};
    const menuItems: ReactNode = switchType(providers);

    return (
        <LoaderSelector menuItems={menuItems} className="Select provider" settings={loaderSettings}/>
    );

}