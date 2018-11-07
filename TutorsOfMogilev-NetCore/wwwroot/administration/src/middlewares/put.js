import axios from 'axios';
import { START, SUCCESS, FAIL } from '../constants';

export default store => next => action => {
  const { put, type, payload, ...rest } = action;

  if (!put) return next(action);

  next({
    ...rest,
    type: type + START
  });

  axios
    .put(`${put}/${payload.id}`, payload.data, {
      headers: {
        'Content-Type': 'application/json'
      }
    })
    .then(response => response.data)
    .then(data => next({ ...rest, type: type + SUCCESS, data, payload }))
    .catch(error => next({ ...rest, type: type + FAIL, error }));
};
