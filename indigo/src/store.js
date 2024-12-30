import { writable } from 'svelte/store';

export const playlists = writable([]);
export const selectedPlaylist = writable(null);
export const song = writable({});

export const currentTrack = writable([]);
export const currentSong = writable({});

export const player = writable(null);
export const playing = writable(false);
export const location = writable(0);
export const volume = writable(50);

export const displayedTrack = writable([]);