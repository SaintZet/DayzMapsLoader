const providerNames: string[] = ["Xam", "Ginfo"];
const mapNames: string[] = ["chernorus", "livonia", "namalsk", "esseker", "takistan", "banov"];

export const MapProviderNameDecorator = (index: string) => providerNames[parseInt(index)];
export const MapNameDecorator = (index: string) => mapNames[parseInt(index)];

