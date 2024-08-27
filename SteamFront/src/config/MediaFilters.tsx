import "../interfaces/games"
import {IMediaItem} from "../interfaces/helpers";

export function filterWebpMedia(mediaArray: IMediaItem[]): IMediaItem[] {
    return mediaArray.filter(item => item.type === 'webp');
}

// export function filterWebpMedia(media: IMediaItem): IMediaItem {
//     return media.
// }