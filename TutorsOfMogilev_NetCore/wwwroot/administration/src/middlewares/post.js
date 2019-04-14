import { START, SUCCESS, FAIL } from '../constants';
import { httpPost } from '../helpers/httpHelper';

export default store => next => action => {
  const { post, type, payload, ...rest } = action;

  if (!post) return next(action);

  next({
    ...rest,
    type: type + START
  });

  httpPost(
    post,
    payload.data,
    data => next({ ...rest, type: type + SUCCESS, data }),
    error => next({ ...rest, type: type + FAIL, error })
  );
};
