import {defaultValue, ProvidedMap, SelectItem} from "../../helpers/types";
import {useContextStore} from "../../modules/DownloadMap/context/providedMapsContext";
import {Selector} from "../../ui/DownloadMap/LoaderSelector";
import {getSelectItems} from "../../modules/DownloadMap/helpers/switchLoaderDataType";
import React, {useEffect, useMemo} from "react";
import * as yup from "yup";
import {Select, SelectChangeEvent} from "@mui/material";
import {useFormik} from "formik";

interface Form {
    typeId: number;
}

const formSchema = yup.object<Form>({
    typeId: yup.number().required().min(0,'Choose map type').default(defaultValue),
}).required()
export default function SelectMapType() {
    const {providedMaps, selectedProvider, selectedMap, setSelectedType} = useContextStore();
    const types = [...new Map(
        providedMaps.filter(x => x.mapProvider.id === selectedProvider && x.map.id === selectedMap)
            .map(x => x.mapType).map(item => [item['name'], item])).values()];
    const options: Set<SelectItem<number>> = getSelectItems(types);

    const formikDefaultProps = useFormik({
        initialValues: formSchema.cast({}) as Form,
        enableReinitialize: true,
        validationSchema: formSchema,
        onSubmit: (form) => {
        }
    });
    const onHandleChange = (event: SelectChangeEvent<number>) => {
        const value = event.target.value as number;
        setSelectedType(value);
        formikDefaultProps.setFieldValue("typeId", value);
    }

    useEffect(() => {
        if (selectedMap === defaultValue){
            formikDefaultProps.resetForm();
        }
    }, [selectedMap])
    return (
        <Selector<Form>
            disabled={selectedMap === -1 || !selectedMap}
            label="Type"
            onHandleChange={onHandleChange}
            {...formikDefaultProps}
            controlName='typeId'
            options={options}
            showError
        />
    );
}