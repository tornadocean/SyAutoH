#pragma once

#include "Errors.h"

/// Should be placed in the appropriate .cpp file somewhere
#define initialiseSingleton( type ) \
  template <> type * Singleton < type > :: mSingleton = 0

#define initialiseTemplateSingleton( temp, type ) \
  template <> temp< type > * Singleton < temp< type > > :: mSingleton = 0

/// To be used as a replacement for initialiseSingleton( )
///  Creates a file-scoped Singleton object, to be retrieved with getSingleton
#define createFileSingleton( type ) \
  initialiseSingleton( type ); \
  type the##type

template < class type > class SERVER_DECL Singleton
{
	public:
		/// Constructor
		Singleton()
		{
			/// If you hit this assert, this singleton already exists -- you can't create another one!
			assert(this->mSingleton == 0);
			this->mSingleton = static_cast<type*>(this);
		}
		/// Destructor
		virtual ~Singleton()
		{
			this->mSingleton = 0;
		}

		inline static type & getSingleton() { assert(mSingleton); return *mSingleton; }
		inline static type* getSingletonPtr() { return mSingleton; }

	protected:

		/// Singleton pointer, must be set to 0 prior to creating the object
		static type* mSingleton;
};
