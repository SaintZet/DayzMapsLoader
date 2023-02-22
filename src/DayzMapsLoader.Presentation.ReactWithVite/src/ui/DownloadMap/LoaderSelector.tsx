import {FormControl, InputLabel, Select, SelectChangeEvent} from "@mui/material";
import {
     SelectItem,
} from "../../helpers/types";
import React, {useMemo} from "react";
import {FormikProps} from "formik";
import FormLabel from "@mui/material/FormLabel";
import Stack from "@mui/material/Stack";
import MenuItem from "@mui/material/MenuItem";
import CircularProgress from "@mui/material/CircularProgress";
import FormHelperText from "@mui/material/FormHelperText";

interface FormikSettings<T>{
    required?: boolean,
    disabled?: boolean,
    showError?: boolean,
    controlName: keyof T & string,
    label: string,
    options: Set<SelectItem<number>>,
    onHandleChange?: (event: SelectChangeEvent<number>) => void,
}

export function Selector<T>(props: FormikProps<T> & FormikSettings<T>) {
    const controlId = `select-${String(props.controlName)}`;
    const meta = useMemo(() => props.getFieldMeta(String(props.controlName)), [props]);
    return <FormControl fullWidth required={!!props.required} disabled={!!props.disabled} error={props.showError}>
        <Stack direction="row" width="100%" spacing={1}>
            <Stack direction="column" justifyContent="center" alignItems="center" width='100%'>
                <FormLabel error={Boolean(meta.error && meta.touched)}>{props.label}</FormLabel>
                {
                    <Select
                        required={!!props.required}
                        disabled={!!props.disabled}
                        id={controlId}
                        name={String(props.controlName)}
                        onChange={props.onHandleChange ?? props.handleChange}
                        onBlur={props.handleBlur}
                        value={props.values[props.controlName] || -1}
                        error={Boolean(meta.error && meta.touched)}
                    >
                        <MenuItem value={-1} disabled={!!props.required}>
                            None
                        </MenuItem>
                        {[...props.options]?.map((x, i) => (
                            <MenuItem key={i} value={x.value}>
                                {x.text}
                            </MenuItem>
                        ))}
                    </Select>
                }
                {props.showError && <FormHelperText>{meta.error}</FormHelperText>}
            </Stack>
        </Stack>
    </FormControl>
};