import Button from "@mui/material/Button";
import CircularProgress from "@mui/material/CircularProgress";
import FormControl from "@mui/material/FormControl";
import FormHelperText from "@mui/material/FormHelperText";
import FormLabel from "@mui/material/FormLabel";
import MenuItem from "@mui/material/MenuItem";
import Stack from "@mui/material/Stack";
import {FormikProps, useFormik} from "formik";
import {AiFillSave} from "react-icons/all";
import {useNavigate} from "react-router-dom";
import SelectProvider from "../../components/DownloadMap/SelectProvider";
import React, {useContext, useMemo} from "react";
import {ProvidedMap} from "../../helpers/types";
import {DownloadMapContext, DownloadMapProvider, useContextStore} from "./context/providedMapsContext";
import SelectMap from "../../components/DownloadMap/SelectMap";
import SelectMapType from "../../components/DownloadMap/SelectMapType";
import * as yup from 'yup'
import Select from "@mui/material/Select";

interface Form {
   /* typeId: number;
    providerId: number;
    zoom: number;*/
    mapId: number;
}

const formSchema = yup.object<Form>({
    /*typeId: yup.number().required().min(0).default(-1),
    providerId: yup.number().required().min(0).default(-1),
    zoom: yup.number().required().min(0).max(8).default(-1)*/
    mapId: yup.number().required().min(0).default(-1),

}).required()

interface SelectItem<T> {
    text: string;
    value: T;
}

const Selector = (props: FormikProps<Form> & {required?: boolean, disabled?: boolean, showError?: boolean, controlName: keyof Form, label: string, options: SelectItem<number>[]}) => {
    const controlId = `select-${props.controlName}`
    const meta = useMemo(() => props.getFieldMeta(props.controlName), [props]);
    return <FormControl fullWidth required={!!props.required} disabled={!!props.disabled} error={props.showError}>
        {Boolean(props.label) && <FormLabel error={Boolean(meta.error && meta.touched)}>{props.label}</FormLabel>}
        <Stack direction="row" width="100%" spacing={1}>
            <Stack direction="column" width='100%'>
                {props.options?.length ? (
                    <Select
                        required={!!props.required}
                        disabled={!!props.disabled}
                        id={controlId}
                        name={props.controlName}
                        onChange={props.handleChange}
                        onBlur={props.handleBlur}
                        value={props.values[props.controlName] || -1}
                        error={Boolean(meta.error && meta.touched)}
                    >
                        <MenuItem value={-1} disabled={!!props.required}>
                            None
                        </MenuItem>
                        {props.options?.map((x, i) => (
                            <MenuItem key={i} value={x.value}>
                                {x.text}
                            </MenuItem>
                        ))}
                    </Select>
                ) : (
                    <CircularProgress />
                )}
                {props.showError && <FormHelperText>{meta.error}</FormHelperText>}
            </Stack>
        </Stack>
    </FormControl>
}


const DownloadMap = () => {
    const navigate = useNavigate();
    const f = useFormik({
        initialValues: formSchema.cast({}) as Form,
        validationSchema: formSchema,
        onSubmit: (form) => {
            console.log(form);
            return Promise.resolve().then(() => navigate('..'));
        }
    })
    const options: SelectItem<number>[] = [
        {text: "1", value: 1}
    ]
    return (
        <DownloadMapProvider>
            <Selector label="Map" {...f} controlName='mapId' options={options} showError/>
            <SelectMap/>
            <SelectMapType/>
            <Button onClick={() => f.submitForm()}>
                <AiFillSave/>
            </Button>
        </DownloadMapProvider>
    );
};

export default DownloadMap;

