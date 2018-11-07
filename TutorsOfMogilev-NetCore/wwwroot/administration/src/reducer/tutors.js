import { TUTOR } from '../constants';
import reducerWithCrud from '../decorators/reducerWithCrud';

const tutors = reducerWithCrud(TUTOR);
export default tutors;
