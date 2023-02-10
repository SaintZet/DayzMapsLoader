import {FormControl, InputLabel, Select} from "@mui/material";
import {
    LoaderTypes,
} from "../../helpers/types";
import React, {ReactNode} from "react";
import {switchType} from "../../modules/DownloadMap/helpers/switchLoaderDataType";
import {useContextStore} from "../../modules/DownloadMap/context/providedMapsContext";

interface LoaderSelectorProps<T> {
    settings?: { m: number, width: number },
    menuItems: ReactNode,
    className: string,
}

export default function LoaderSelector<T extends LoaderTypes>(props: LoaderSelectorProps<T>): JSX.Element {
    return (
        <div className={props.className}>
            <FormControl sx={props.settings}>
                <InputLabel id={props.className}>{props.className}</InputLabel>
                <Select labelId={props.className} label={props.className}>
                    {props.menuItems}
                </Select>
            </FormControl>
        </div>
    )
}