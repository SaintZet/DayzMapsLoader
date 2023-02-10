import {FormControl, InputLabel, Select} from "@mui/material";
import {
    LoaderSelectorTypes,
} from "../../helpers/types";
import React, {ReactNode} from "react";
import {switchType} from "../../modules/DownloadMap/helpers/switchLoaderDataType";

interface LoaderSelectorProps<T> {
    settings?: { m: number, width: number },
    menuItems: ReactNode,
    className: string,
}

export default function LoaderSelector<T extends LoaderSelectorTypes>(props: LoaderSelectorProps<T>): JSX.Element {
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