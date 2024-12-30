<script>
    import "../css/main.css"
    import TopBar from "../components/TopBar.svelte";
    import { updatePlaylists } from "../lib";
    import { selectedPlaylist } from "../store";
    import { onMount } from "svelte";
    import { writable } from "svelte/store";
    import Playlist from "../components/Playlist.svelte";
    import Library from "../components/Library.svelte";
    import PlaylistPanel from "../components/PlaylistPanel.svelte";
    import MediaPlayer from "../components/MediaPlayer.svelte";

    export const player = writable(null);
    export const playing = writable(false);
    export const location = writable(0);

    onMount(() => {
        updatePlaylists();
        
        $player = new Audio();
    })
</script>

<div class="main-page">
    <div class="centered-side">
        <TopBar />
    </div>

    <Library></Library>

    <div class="centered-side-alt">
        {#if $selectedPlaylist}
            <PlaylistPanel></PlaylistPanel>
        {/if}
    </div>

    <MediaPlayer></MediaPlayer>
</div>