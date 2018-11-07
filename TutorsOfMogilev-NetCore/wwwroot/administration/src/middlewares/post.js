import axios from 'axios';
import { START, SUCCESS, FAIL } from '../constants';

export default store => next => action => {
  const { post, type, payload, ...rest } = action;

  if (!post) return next(action);

  next({
    ...rest,
    type: type + START
  });

  axios
    .post(post, payload.data, {
      headers: {
        'Content-Type': 'application/json'
      }
    })
    .then(response => response.data)
    .then(data => next({ ...rest, type: type + SUCCESS, data }))
    .catch(error => next({ ...rest, type: type + FAIL, error }));
};
