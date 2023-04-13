//
import React, { useEffect, useState } from "react";
import { getAllVideos } from "../modules/videoManager";

const VideoList = () => {
  const [videos, setVideos] = useState([]); // returns an array: [stateVariable, setterFunction]
  //use state takes one argument that will be the initial value

  const getVideos = () => {
    getAllVideos().then((videos) => setVideos(videos));
  };

  useEffect(() => {
    //invoking getVideos inside a useEffect
    //this func will run on the initial render of the component
    //keeps it from repeatedly running when the state changes
    getVideos();
  }, []);

  return (
    <div className="container">
      <div className="row justify-content-center"></div>
      {videos.map((video) => (
        <>
          <div key={video.id}>{video.title}</div>
          <Video video={video} key={video.id} />
        </>
      ))}
    </div>
  );
};

//export default, bc its the only one on this module, can also put export on const VideoList
export default VideoList;
//can only have one default export per module
