import {useContextStore} from "../../modules/DownloadMap/context/providedMapsContext";
import {Selector} from "../../ui/DownloadMap/LoaderSelector";
import {getSelectItems} from "../../modules/DownloadMap/helpers/switchLoaderDataType";
import React, {ReactNode, useContext, useEffect, useMemo, useState} from "react";
import * as yup from "yup";
import {useFormik} from "formik";
import {defaultValue, SelectItem} from "../../helpers/types";
import {SelectChangeEvent} from "@mui/material";

interface Form {
    mapId: number;
}

const formSchema = yup.object<Form>({
    mapId: yup.number().required().min(0, 'Choose map').default(defaultValue),
}).required()

export default function SelectMap() {
    const {providedMaps, selectedProvider, setSelectedMap} = useContextStore();
    const maps = [...new Map(
        providedMaps.filter(x => x.mapProvider.id === selectedProvider)
            .map(x => x.map).map(item => [item['name'], item])).values()];
    const options: Set<SelectItem<number>> = getSelectItems(maps);

    const formikDefaultProps = useFormik({
        initialValues: formSchema.cast({value: -1}) as Form,
        validationSchema: formSchema,
        onSubmit: (form) => {
        }
    });
    const onHandleChange = (event: SelectChangeEvent<number>) => {
        const value = event.target.value as number;
        setSelectedMap(value);
        formikDefaultProps.setFieldValue("mapId", value);
    }

    useEffect(()=>{
        if (selectedProvider === defaultValue){
            formikDefaultProps.resetForm();
        }
    }, [selectedProvider])

    return (
        <Selector<Form>
            disabled={selectedProvider === defaultValue || !selectedProvider}
            label="Map"
            onHandleChange={onHandleChange}
            {...formikDefaultProps}
            controlName='mapId'
            options={options}
            showError
        />
    );

}