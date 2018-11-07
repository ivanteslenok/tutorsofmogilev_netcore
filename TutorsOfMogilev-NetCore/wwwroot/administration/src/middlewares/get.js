import axios from 'axios';
import { START, SUCCESS, FAIL } from '../constants';

export default store => next => action => {
  const { get, type, ...rest } = action;

  if (!get) return next(action);

  next({
    ...rest,
    type: type + START
  });

  axios
    .get(get)
    .then(response => response.data)
    .then(data => next({ ...rest, type: type + SUCCESS, data }))
    .catch(error => next({ ...rest, type: type + FAIL, error }));
};
