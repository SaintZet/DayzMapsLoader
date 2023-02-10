import {Map, MapType} from "../../helpers/types";
import {useContextStore} from "../../modules/DownloadMap/context/providedMapsContext";
import LoaderSelector from "../../ui/DownloadMap/LoaderSelector";
import {switchType} from "../../modules/DownloadMap/helpers/switchLoaderDataType";
import React, {ReactNode, useMemo} from "react";

export default function SelectMapType() {
    const {providedMaps} = useContextStore();
    const types = useMemo(() => providedMaps.map(x => x.mapType), [providedMaps]);
    const loaderSettings = {m: 3, width: 300};
    const menuItems: ReactNode = switchType(types);

    return (
        <LoaderSelector menuItems={menuItems} className="Select map type" settings={loaderSettings}/>
    );

}