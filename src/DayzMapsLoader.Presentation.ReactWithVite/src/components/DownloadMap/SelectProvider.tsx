import {useContextStore} from "../../modules/DownloadMap/context/providedMapsContext";
import LoaderSelector from "../../ui/DownloadMap/LoaderSelector";
import {switchType} from "../../modules/DownloadMap/helpers/switchLoaderDataType";
import React, {ReactNode, useMemo} from "react";


export default function SelectProvider() {
    const {providedMaps} = useContextStore();
    const providers = useMemo(() => [...new Map(providedMaps.map(x => x.mapProvider).map(item => [item['name'], item])).values()], [providedMaps]);
    const loaderSettings = {m: 3, width: 300};
    const menuItems: ReactNode = switchType(providers);

    return (
        <LoaderSelector menuItems={menuItems} className="Select provider" settings={loaderSettings}/>
    );

}