<script>
    import "../css/main.css"
    import Icon from "@iconify/svelte"
    import { selectedPlaylist, currentTrack, displayedTrack } from "../store";
    import { writable } from "svelte/store";
    import { getSongs, updatePlaylists } from "../lib"

    export let playlist = {};

    function setSelected() {
        $selectedPlaylist = playlist;
        $displayedTrack = [];
        getSongs();
        updatePlaylists();
    }
</script>

<div class="playlist" on:click={() => setSelected()}>
    <div class="centered-side">
        <p>{playlist.name}</p>
    </div>
</div>