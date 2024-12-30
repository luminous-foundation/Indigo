<script>
    import { onMount } from "svelte";
    import { selectedPlaylist, player, playing, currentTrack, currentSong, displayedTrack, location } from "../store";
    import { preventDefault } from "svelte/legacy";
    import { SvelteURL } from "svelte/reactivity";

    onMount(() => {
        getSongs();
    });

    function getSongs() {
        fetch("http://localhost:24896/loader", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify($selectedPlaylist)
        }).then(resp => resp.json()).then(obj => {
            $displayedTrack = obj.data;
        })
    }

    export async function loadPlayAudio(song) {
        $currentTrack = $displayedTrack;
        $currentSong = song;

        if($player == null) {
            let audio = new Audio();
            
            audio.addEventListener('ended', () => {
                audio.pause();
                
                let song = seekNext();
                loadPlayAudio(song);
            });

            audio.ontimeupdate = function() {
                $location = $player.currentTime
            }

            $player = audio;
        }

        let buff = (await fetch("http://localhost:24896/songs", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(song)
        }));

        let audioBlob = await buff.blob();
        let audioUrl = SvelteURL.createObjectURL(audioBlob);

        $player.src = audioUrl;

        $playing = true;
        $player.play();
    }
</script>

<div class="playlist-panel">
    <div class="centered-down">
        {#each $displayedTrack as s}
        <div class="song" on:click={async () => await loadPlayAudio(s)}>
            <p>{s.title}</p>
        </div>
        {/each}
    </div>
</div>

<style>
    .song {
        padding-right: 10px;
        padding-left: 10px;
        border-radius: 30px;
    }
    .song:hover {
        background-color: #170d33;
        border: 1px solid white;
        cursor: pointer;
    }
</style>