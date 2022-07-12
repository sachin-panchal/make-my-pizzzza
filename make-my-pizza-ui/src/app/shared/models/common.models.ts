
export interface ClientResponse {
    data: any;
    success: boolean;
    errors: string[];
}

export enum SnackbarType {
    info,
    success,
    warning,
    error
}

export interface SnackbarMessage {
    messages: string[];
    type: SnackbarType;
}

export class Configuration {
    webApiUrl!: string;
    logging!: Logging;
    environment!: Environment;
}

export interface Logging {
    console: boolean
}

export interface Environment {
    name: string
}

export interface ImageSlider {
    img: string;
    alt: string;
    text: string;
  }