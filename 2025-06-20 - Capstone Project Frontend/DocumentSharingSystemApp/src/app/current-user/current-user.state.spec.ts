import { TestBed } from '@angular/core/testing';
import {  provideStore,  Store } from '@ngxs/store';
import { CurrentUserState, CurrentUserStateModel } from './current-user.state';
import { SetCurrentUserAction } from './current-user.actions';
import { UserModel } from '../models/user.model';

describe('CurrentUser store', () => {
  let store: Store;
  beforeEach(() => {
    TestBed.configureTestingModule({
       providers: [provideStore([CurrentUserState])]
      
    });

    store = TestBed.inject(Store);
    store.reset({});
  });

  it('should create an action and set an item', () => {
    let user = new UserModel();
    const expected: CurrentUserStateModel = {
      user : user
    };
    store.dispatch(new SetCurrentUserAction(user));
    const actual = store.selectSnapshot(CurrentUserState.getUser);
    expect(actual).toEqual(user);
  });

  it('should update user data', () => {
    const user1 = new UserModel("1");
    const user2 = new UserModel("2");
     let callCount = 0;

     store.dispatch(new SetCurrentUserAction(user1));
  store.select(CurrentUserState.getUser).subscribe((data) => {
    callCount++;
    if (callCount === 1) {
      expect(data).toEqual(user1);
      // Dispatch next action
      store.dispatch(new SetCurrentUserAction(user2));
    } else if (callCount === 2) {
      expect(data).toEqual(user2);
  
    }
  });

  });


});
