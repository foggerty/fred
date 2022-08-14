// I should be able to justify every line of code and every design decision.

// Nothing should be coupled to any specific implimentation.

// Always test against interfaces, even if you only have a single implementation.

// Only use libraries if they cut down on boiler-plate code (e.g. automapper) or do
// something unique like image-manipulations libraries.  Otherwise, RYO.

// Go over design - will it still compose the same way if the 
// transport layer is switched to sockets?  Does the http part
// make assumptions about transport that sockets doesn't support?

// Re-red "Eight fallacys of distributed computing" - what can Fred do to help
// people avoid, or not really applicable?

// Always return 200.  404 is a resource code, i.e. the HTTP resource
// that you're trying to reach doesn't exist.  To extend it to RPC doesn't make sense, as it's a transport
// concern.  The answer will have a boolean or enum to indicate the result.

// Can I use async/await with Task?  Can the thread-pool make use of it?

// Challenge: if I want to use Fred to host a standard REST api, how easy is it?
// Maybe just have it return a IHttpAnswer.....?  

// Should be easy to add before/after for things like audit logging at both API
// and endpoint level.

// Config should have defaults, run with --create-config to serialise to 
// a default config file.

// Must be able to impelement "swagger" like functionality using standard endpoint behavior.

// The threadpool etc, in fact a generic collection pool, should be available as services at 
// the endpoint level.

// Make sure api/endpoint paths are unique.

// Add a carertaker thread.

// Add a X509StoreService (or certificate service).

// Session management - don't think it should be supported.  A) it's a security concern, b) it's
// a pain to implement and may lead to assumptions that HTTP will always be the transport and c)
// I feel that it's only a thing in Web APIs because of the legacy of HTTP. 

// Every time that I find myself thinking "asp.net has this, so should Fred", STOP, and ask why
// it has it in the first place.  Maybe it's like HttpContext - i.e. a leaky abstraction -
// so figure out WHY it's used, and ensure that it's not there because of the reliance on HTTP.

// Really try to push home that "static = evil" just means that you don't know how to
// use them properly.  i.e. as a way around the short-commings of traditional OO development
// practices.

// Swap our JSON with msgPack at some point - do a video of writing a serializer?  So first
// use standard library, then write own.  Benchmark the two.  