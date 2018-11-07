import axios from 'axios';
import { START, SUCCESS, FAIL } from '../constants';

export default store => next => action => {
  const { del, type, payload, ...rest } = action;

  if (!del) return next(action);

  next({
    ...rest,
    type: type + START
  });

  axios
    .delete(`${del}/${payload.id}`)
    .then(next({ ...rest, type: type + SUCCESS, payload }))
    .catch(error => next({ ...rest, type: type + FAIL, error }));
};
