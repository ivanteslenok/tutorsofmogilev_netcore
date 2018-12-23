import { CONTACT_TYPE } from '../constants';
import reducerWithCrud from '../decorators/reducerWithCrud';

const contactTypes = reducerWithCrud(CONTACT_TYPE);
export default contactTypes;
