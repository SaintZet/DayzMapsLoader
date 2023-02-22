import {DownloadMapProvider, useContextStore} from "../../modules/DownloadMap/context/providedMapsContext";
import {getSelectItems} from "../../modules/DownloadMap/helpers/switchLoaderDataType";
import React, {ChangeEvent, ReactNode, useMemo} from "react";
import {defaultValue, MapProvider, SelectItem} from "../../helpers/types";
import {useFormik} from "formik";
import * as yup from "yup";
import {Selector} from "../../ui/DownloadMap/LoaderSelector";
import {SelectChangeEvent} from "@mui/material";

interface Form {
    providerId: number;
}

const formSchema = yup.object<Form>({
    providerId: yup.number().required().min(0, 'Choose map provider ').default(defaultValue),
}).required()

export default function SelectProvider() {
    const {providedMaps, setSelectedProvider, setSelectedMap} = useContextStore();
    const providers = useMemo(() => [...new Map(providedMaps.map(x => x.mapProvider).map(item => [item['name'], item])).values()], [providedMaps]);
    const options: Set<SelectItem<number>> = getSelectItems(providers);
    const formikDefaultProps = useFormik({
        initialValues: formSchema.cast({}) as Form,
        validationSchema: formSchema,
        enableReinitialize: true,
        onSubmit: (form) => {
        },
    });

    const onHandleChange = (event: SelectChangeEvent<number>) => {
        const value = event.target.value as number;
        setSelectedProvider(value);
        setSelectedMap(defaultValue);
        formikDefaultProps.setFieldValue("providerId", value);
    }

    return (
        <Selector<Form> onHandleChange={onHandleChange} label="Provider" {...formikDefaultProps} controlName='providerId' options={options} showError />
    );
}