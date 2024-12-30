import { playlists, player, playing, currentTrack, currentSong, selectedPlaylist, displayedTrack } from "./store";


let track = {};
currentTrack.subscribe(val => {
    track = val;
});

let song = {};
currentSong.subscribe(val => {
    song = val;
})

let selected = {};
selectedPlaylist.subscribe(val => {
    selected = val;
})


export function updatePlaylists() {
    fetch("http://localhost:24896/playlists").then(resp => resp.json()).then(obj => {
        playlists.set(obj.data);
    })
}

export function seekNext() {
    /*if(looped) {

    }*/
    for(let i = 0; i < track.length; i++) {
        if(track[i].title === song.title) {
            if(!(i + 1 >= track.length)) {
                console.log(track[i + 1]);
                return track[i + 1];
            } else {
                console.log(track[0]);
                return track[0];
            }
        }
    }

    return null;
}

export function getSongs() {
    fetch("http://localhost:24896/loader", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(selected)
    }).then(resp => resp.json()).then(obj => {
        displayedTrack.set(obj.data);
    })
}