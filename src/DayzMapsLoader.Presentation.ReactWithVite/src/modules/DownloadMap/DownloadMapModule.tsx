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
import {ProvidedMap, SelectItem} from "../../helpers/types";
import {DownloadMapContext, DownloadMapProvider, useContextStore} from "./context/providedMapsContext";
import SelectMap from "../../components/DownloadMap/SelectMap";
import SelectMapType from "../../components/DownloadMap/SelectMapType";
import * as yup from 'yup'
import Select from "@mui/material/Select";
import {Selector} from "../../ui/DownloadMap/LoaderSelector";

interface Form {
    mapId: number;
}

const formSchema = yup.object<Form>({
    mapId: yup.number().required().min(0).default(-1),
}).required()


const DownloadMapModule = () => {
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
            {/*<Selector<Form> label="Map" {...f} controlName='mapId' options={options} showError/>*/}
            <SelectProvider/>
            <SelectMap/>
        </DownloadMapProvider>
    );
};

export default DownloadMapModule;

