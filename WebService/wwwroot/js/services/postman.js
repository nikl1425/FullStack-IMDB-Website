define([], () => {
  let subscribers = [];
  let lastEvent = undefined;

  let publish = (event, data) => {
      subscribers.filter(x => x.event === event)
          .forEach(x => x.callback(data));
      lastEvent = { event, data };
  }

  let subscribe = (event, callback) => {
      let subscriber = { event, callback };
      subscribers.push(subscriber);

      if (lastEvent && lastEvent.event === event) {
          callback(lastEvent.data);
          lastEvent = undefined;
      }

      return () => {
          subscribers = subscribers.filter(x => x !== subscriber);
      }
  }


  return {
      subscribe,
      publish
  }
});