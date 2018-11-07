import { SUBJECT } from '../constants';
import reducerWithCrud from '../decorators/reducerWithCrud';

const subjects = reducerWithCrud(SUBJECT);
export default subjects;
