export interface GeneratedImage {
    id?: string;
    image?: string;
    promtId?: string
    customPrompt?: string;
}

export interface GeneratedImageResponse {
    id?: string;
    image?: string;
    address?: string
}

export interface Prompt {
    id?: string;
    name?: string;
    image?: string;
    custom?: boolean;
}
