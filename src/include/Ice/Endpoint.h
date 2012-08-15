// **********************************************************************
//
// Copyright (c) 2003-2011 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************
//
// Ice version 3.4.2
//
// <auto-generated>
//
// Generated from file `Endpoint.ice'
//
// Warning: do not edit this file.
//
// </auto-generated>
//

#ifndef __Ice_Endpoint_h__
#define __Ice_Endpoint_h__

#include <Ice/LocalObjectF.h>
#include <Ice/ProxyF.h>
#include <Ice/ObjectF.h>
#include <Ice/Exception.h>
#include <Ice/LocalObject.h>
#include <Ice/Proxy.h>
#include <IceUtil/ScopedArray.h>
#include <Ice/BuiltinSequences.h>
#include <Ice/EndpointF.h>
#include <Ice/UndefSysMacros.h>

#ifndef ICE_IGNORE_VERSION
#   if ICE_INT_VERSION / 100 != 304
#       error Ice version mismatch!
#   endif
#   if ICE_INT_VERSION % 100 > 50
#       error Beta header file detected
#   endif
#   if ICE_INT_VERSION % 100 < 2
#       error Ice patch level mismatch!
#   endif
#endif

#ifndef ICE_API
#   ifdef ICE_API_EXPORTS
#       define ICE_API ICE_DECLSPEC_EXPORT
#   else
#       define ICE_API ICE_DECLSPEC_IMPORT
#   endif
#endif

namespace Ice
{

class EndpointInfo;
bool operator==(const EndpointInfo&, const EndpointInfo&);
bool operator<(const EndpointInfo&, const EndpointInfo&);

class Endpoint;
bool operator==(const Endpoint&, const Endpoint&);
bool operator<(const Endpoint&, const Endpoint&);

class IPEndpointInfo;
bool operator==(const IPEndpointInfo&, const IPEndpointInfo&);
bool operator<(const IPEndpointInfo&, const IPEndpointInfo&);

class TCPEndpointInfo;
bool operator==(const TCPEndpointInfo&, const TCPEndpointInfo&);
bool operator<(const TCPEndpointInfo&, const TCPEndpointInfo&);

class UDPEndpointInfo;
bool operator==(const UDPEndpointInfo&, const UDPEndpointInfo&);
bool operator<(const UDPEndpointInfo&, const UDPEndpointInfo&);

class OpaqueEndpointInfo;
bool operator==(const OpaqueEndpointInfo&, const OpaqueEndpointInfo&);
bool operator<(const OpaqueEndpointInfo&, const OpaqueEndpointInfo&);

}

namespace IceInternal
{

ICE_API ::Ice::LocalObject* upCast(::Ice::EndpointInfo*);

ICE_API ::Ice::LocalObject* upCast(::Ice::Endpoint*);

ICE_API ::Ice::LocalObject* upCast(::Ice::IPEndpointInfo*);

ICE_API ::Ice::LocalObject* upCast(::Ice::TCPEndpointInfo*);

ICE_API ::Ice::LocalObject* upCast(::Ice::UDPEndpointInfo*);

ICE_API ::Ice::LocalObject* upCast(::Ice::OpaqueEndpointInfo*);

}

namespace Ice
{

typedef ::IceInternal::Handle< ::Ice::EndpointInfo> EndpointInfoPtr;

typedef ::IceInternal::Handle< ::Ice::Endpoint> EndpointPtr;

typedef ::IceInternal::Handle< ::Ice::IPEndpointInfo> IPEndpointInfoPtr;

typedef ::IceInternal::Handle< ::Ice::TCPEndpointInfo> TCPEndpointInfoPtr;

typedef ::IceInternal::Handle< ::Ice::UDPEndpointInfo> UDPEndpointInfoPtr;

typedef ::IceInternal::Handle< ::Ice::OpaqueEndpointInfo> OpaqueEndpointInfoPtr;

}

namespace Ice
{

const ::Ice::Short TCPEndpointType = 1;

const ::Ice::Short UDPEndpointType = 3;

}

namespace Ice
{

class ICE_API EndpointInfo : virtual public ::Ice::LocalObject
{
public:

    typedef EndpointInfoPtr PointerType;
    
    EndpointInfo() {}
    EndpointInfo(::Ice::Int, bool);

    virtual ::Ice::Short type() const = 0;

    virtual bool datagram() const = 0;

    virtual bool secure() const = 0;

    ::Ice::Int timeout;

    bool compress;
};

inline bool operator==(const EndpointInfo& l, const EndpointInfo& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) == static_cast<const ::Ice::LocalObject&>(r);
}

inline bool operator<(const EndpointInfo& l, const EndpointInfo& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) < static_cast<const ::Ice::LocalObject&>(r);
}

class ICE_API Endpoint : virtual public ::Ice::LocalObject
{
public:

    typedef EndpointPtr PointerType;
    

    virtual ::std::string toString() const = 0;

    virtual ::Ice::EndpointInfoPtr getInfo() const = 0;
};

inline bool operator==(const Endpoint& l, const Endpoint& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) == static_cast<const ::Ice::LocalObject&>(r);
}

inline bool operator<(const Endpoint& l, const Endpoint& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) < static_cast<const ::Ice::LocalObject&>(r);
}

class ICE_API IPEndpointInfo : public ::Ice::EndpointInfo
{
public:

    typedef IPEndpointInfoPtr PointerType;
    
    IPEndpointInfo() {}
    IPEndpointInfo(::Ice::Int, bool, const ::std::string&, ::Ice::Int);

    ::std::string host;

    ::Ice::Int port;
};

inline bool operator==(const IPEndpointInfo& l, const IPEndpointInfo& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) == static_cast<const ::Ice::LocalObject&>(r);
}

inline bool operator<(const IPEndpointInfo& l, const IPEndpointInfo& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) < static_cast<const ::Ice::LocalObject&>(r);
}

class ICE_API TCPEndpointInfo : public ::Ice::IPEndpointInfo
{
public:

    typedef TCPEndpointInfoPtr PointerType;
    
    TCPEndpointInfo() {}
    TCPEndpointInfo(::Ice::Int, bool, const ::std::string&, ::Ice::Int);
};

inline bool operator==(const TCPEndpointInfo& l, const TCPEndpointInfo& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) == static_cast<const ::Ice::LocalObject&>(r);
}

inline bool operator<(const TCPEndpointInfo& l, const TCPEndpointInfo& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) < static_cast<const ::Ice::LocalObject&>(r);
}

class ICE_API UDPEndpointInfo : public ::Ice::IPEndpointInfo
{
public:

    typedef UDPEndpointInfoPtr PointerType;
    
    UDPEndpointInfo() {}
    UDPEndpointInfo(::Ice::Int, bool, const ::std::string&, ::Ice::Int, ::Ice::Byte, ::Ice::Byte, ::Ice::Byte, ::Ice::Byte, const ::std::string&, ::Ice::Int);

    ::Ice::Byte protocolMajor;

    ::Ice::Byte protocolMinor;

    ::Ice::Byte encodingMajor;

    ::Ice::Byte encodingMinor;

    ::std::string mcastInterface;

    ::Ice::Int mcastTtl;
};

inline bool operator==(const UDPEndpointInfo& l, const UDPEndpointInfo& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) == static_cast<const ::Ice::LocalObject&>(r);
}

inline bool operator<(const UDPEndpointInfo& l, const UDPEndpointInfo& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) < static_cast<const ::Ice::LocalObject&>(r);
}

class ICE_API OpaqueEndpointInfo : public ::Ice::EndpointInfo
{
public:

    typedef OpaqueEndpointInfoPtr PointerType;
    
    OpaqueEndpointInfo() {}
    OpaqueEndpointInfo(::Ice::Int, bool, const ::Ice::ByteSeq&);

    ::Ice::ByteSeq rawBytes;
};

inline bool operator==(const OpaqueEndpointInfo& l, const OpaqueEndpointInfo& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) == static_cast<const ::Ice::LocalObject&>(r);
}

inline bool operator<(const OpaqueEndpointInfo& l, const OpaqueEndpointInfo& r)
{
    return static_cast<const ::Ice::LocalObject&>(l) < static_cast<const ::Ice::LocalObject&>(r);
}

}

#endif
