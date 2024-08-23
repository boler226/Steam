import "../interfaces/games"
import {IMediaItem} from "../interfaces/games";

export function filterWebpMedia(mediaArray: IMediaItem[]): IMediaItem[] {
    return mediaArray.filter(item => item.type === 'webp');
}