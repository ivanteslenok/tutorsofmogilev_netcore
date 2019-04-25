import { START, SUCCESS, FAIL } from '../constants';
import { httpPost } from '../helpers/httpHelper';

export default store => next => async action => {
  const { post, type, payload, ...rest } = action;

  if (!post) return next(action);

  next({
    ...rest,
    type: type + START
  });

  try {
    const data = await httpPost(post, payload.data);
    next({ ...rest, type: type + SUCCESS, data });
  } catch (error) {
    next({ ...rest, type: type + FAIL, error });
  }
};
