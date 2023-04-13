const baseUrl = "/api/video";
// pulls in from the controller
export const getAllVideos = () => {
  return fetch(baseUrl).then((res) => res.json());
};

export const addVideo = (video) => {
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(video),
  });
};
