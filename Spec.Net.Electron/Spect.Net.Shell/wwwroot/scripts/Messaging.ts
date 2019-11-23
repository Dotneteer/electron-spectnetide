/**
 * This interface represents an abstract message type that can be
 * either a request or a response.
 */
export interface AbstractMessage {
    command: string;
    correlationId?: number;
}