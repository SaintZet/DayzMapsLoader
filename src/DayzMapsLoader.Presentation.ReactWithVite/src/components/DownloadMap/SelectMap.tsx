import {useContextStore} from "../../modules/DownloadMap/context/providedMapsContext";
import LoaderSelector from "../../ui/DownloadMap/LoaderSelector";
import {switchType} from "../../modules/DownloadMap/helpers/switchLoaderDataType";
import React, {ReactNode, useMemo} from "react";

export default function SelectMap() {
    const {providedMaps} = useContextStore();
    const maps = useMemo(() => providedMaps.map(x => x.map), [providedMaps]);
    const loaderSettings = {m: 3, width: 300};
    const menuItems: ReactNode = switchType(maps);

    return (
        <LoaderSelector menuItems={menuItems} className="Select map" settings={loaderSettings}/>
    );

}